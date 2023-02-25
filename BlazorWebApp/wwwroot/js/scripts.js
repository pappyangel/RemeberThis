
var loadFileJS = function () {    
    
    var image = document.getElementById('pageimage');   
    var selectedFile = document.getElementById('blazIF1').files[0];    
    image.src = URL.createObjectURL(selectedFile);

};

var ResetFilePicker = function () {        
    
    document.getElementById("blazIF1").value = "";    

};
