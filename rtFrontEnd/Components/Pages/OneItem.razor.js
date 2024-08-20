
// function to load the image file into the image element
export function loadFileJS() {

    var image = document.getElementById('pageimage');
    var selectedFile = document.getElementById('blazIF1').files[0];
    image.src = URL.createObjectURL(selectedFile);

};

//  function to reset the file picker if an invalid file was originally chosen
export function ResetFilePicker() {

    document.getElementById("blazIF1").value = "";

};

// unknow if setImage function is needed
window.setImage = async (imageElementId, imageStream) => {
    const arrayBuffer = await imageStream.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const image = document.getElementById(imageElementId);
    image.onload = () => {
        URL.revokeObjectURL(url);
    }
    image.src = url;
}

//  function to display message as test
export function displayMessage(message) {
    alert('This was your message:' + message);
}