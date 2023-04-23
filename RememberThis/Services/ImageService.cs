using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RememberThis.Services;

public class ImageService
{
    private readonly ILogger<ImageService> _logger;
    private string ImageServiceMsg = "Controller Start";
    private bool LoggingEnabled = true;
    private string[] permittedExtensions = new string[] { ".gif", ".png", ".jpg", ".jpeg" };
    public ImageService(ILogger<ImageService> logger)
    {
        _logger = logger;

    }
    public bool IsValidFileExtensionAndSignature(string fileName, MemoryStream streamParam)
    {

        // might need to go somewhere higher in stack
        ImageServiceMsg = "Image Service Validation Method Start";
        if (LoggingEnabled)
            _logger.LogInformation(ImageServiceMsg);

        MemoryStream data = new MemoryStream();
        streamParam.Position = 0;
        streamParam.CopyTo(data);

        // this is generally checked by the file upload control itself - but we can double check it here
        if (data == null || data.Length == 0)
        {
            ImageServiceMsg = "file empty";
            if (LoggingEnabled)
                _logger.LogInformation(ImageServiceMsg);
            return false;
        }

        var filenameonly = Path.GetFileNameWithoutExtension(fileName);
        if (string.IsNullOrEmpty(filenameonly))
        {
            ImageServiceMsg = "file name not valid";
            if (LoggingEnabled)
                _logger.LogInformation(ImageServiceMsg);
            return false;
        }

        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
        {
            ImageServiceMsg = "file extension not valid";
            if (LoggingEnabled)
                _logger.LogInformation(ImageServiceMsg);
            return false;
        }

        data.Position = 0;

        using (var reader = new BinaryReader(data))
        {
            var signatures = _fileSignature[ext];
            var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

            bool fileSigCorrect = signatures.Any(signature =>
                headerBytes.Take(signature.Length).SequenceEqual(signature));

            ImageServiceMsg = fileSigCorrect ? "file check good" : "file signiture invalid";
            if (LoggingEnabled)
                _logger.LogInformation(ImageServiceMsg);

            return fileSigCorrect;

        }

    } // End IsValidFileExtensionAndSignature

    private static readonly Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
        {
            { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                }
            }
        };

}
