/* jshint esversion:6, browser:true, devel:true */
(function () {
    "use strict";
    let fileUploadInput = document.getElementById("file-upload-input");
    fileUploadInput.addEventListener("change", fileInputChanged, false);

    let uploadedImage = document.getElementById("uploaded-image");
    uploadedImage.addEventListener("load", showUploadedImage, false);

    let spinner = document.getElementById("spinner");
    let errorDisplay = document.getElementById("error-display");

    function fileInputChanged(event) {
        showSpinner();
        let files = event.target.files;
        upload(files[0]); //TODO: support multiple files (https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch#Uploading_multiple_files)
    }

    function upload(file) {
        let formData = new FormData();
        formData.append("file", file);
        fetch("", { method: "POST", body: formData })
            .then(response => response.json().then(response => uploadedImage.src = response.src))
            .catch(error => handle(error));
    }

    function showSpinner() {
        show(spinner);
        hide(uploadedImage);
        hide(errorDisplay);
    }

    function showUploadedImage() {
        hide(spinner);
        hide(errorDisplay);
        show(uploadedImage);
    }

    function handle(error) {
        console.error(error);
        show(errorDisplay);
        hide(spinner);
        hide(uploadedImage);
    }

    function show(element) {
        element.style.display = "initial";
    }

    function hide(element) {
        element.style.display = "none";
    }
})();