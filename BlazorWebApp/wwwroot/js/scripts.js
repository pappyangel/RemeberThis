var ShowAlert = function (message) {
    alert(message);
}


var loadFileJS = function (event) {
    alert("Inside loadFileJS");
    var image = document.getElementById('pageimage');    
    alert(image.src);
    alert(event.File.Name);
    // alert(event.target.files[0].);
    // image.src = URL.createObjectURL(event.target.files[0]);
    image.src = URL.createObjectURL(event.File);
    
    alert("Leaving loadFileJS");
};

var loadFile2 = function (event) {    
    var image = document.getElementById('pageimage');        
    
    image.src = URL.createObjectURL(event.target.files[0]);    
};

var BlazorUniversity = Blaz


var BlazorUniversity = BlazorUniversity || {};

BlazorUniversity.setDocumentTitle = function (title) {
    document.title = title;
};
