
var loadFileJS = function () {    
    
    var image = document.getElementById('pageimage');   
    var selectedFile = document.getElementById('IF1').files[0];    
    image.src = URL.createObjectURL(selectedFile);

};

window.enablePopovers = function () {
    document.querySelectorAll('[data-bs-toggle="popover"]')
        .forEach(function (popover) {
            new bootstrap.Popover(popover);
        });
}

