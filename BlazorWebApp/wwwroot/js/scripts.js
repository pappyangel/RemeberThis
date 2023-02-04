var ShowAlert = function (message) {
    alert(message);
}


var loadFileJS = function (event) {
    // alert("Inside loadFileJS");
    var image = document.getElementById('pageimage');   
    var selectedFile = document.getElementById('IF1').files[0];
    // alert(image.src);
    // alert(URL.createObjectURL(selectedFile));
    // alert(event.target.files[0].);
    // image.src = URL.createObjectURL(event.target.files[0]);
    image.src = URL.createObjectURL(selectedFile);
    
    // alert("Leaving loadFileJS");
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
