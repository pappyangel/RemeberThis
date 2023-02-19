
// const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]')
// const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl))


// var popover = new bootstrap.Popover(document.querySelector('.example-popover'), {
//     container: 'body'
//   })

//   var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
// var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
//   return new bootstrap.Popover(popoverTriggerEl)
// })

var loadFileJS = function () {    
    
    var image = document.getElementById('pageimage');   
    var selectedFile = document.getElementById('IF1').files[0];    
    image.src = URL.createObjectURL(selectedFile);

};

// $(function () {
//     $('[data-bs-toggle="popover"]').popover()
//   })

// $(document).ready(function() {
//     $('[data-bs-toggle=popover]').popover();
// }); 


