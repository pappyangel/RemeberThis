
var loadFileJS = function () {    
    
    var image = document.getElementById('pageimage');   
    var selectedFile = document.getElementById('blazIF1').files[0];    
    image.src = URL.createObjectURL(selectedFile);

};

var ResetFilePicker = function () {        
    
    document.getElementById("blazIF1").value = "";    

};

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
