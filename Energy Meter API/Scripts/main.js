function uploadFile() {
    var fileUpload = $("#file").get(0);
    var files = fileUpload.files;

    // Create FormData object  
    var fileData = new FormData();

    fileData.append(files[0].name, files[0]);

    $.ajax({
        url: '/Meter/UploadReadings',
        type: "POST",
        contentType: false,
        processData: false,
        data: fileData,
        success: function (result) {
            $("#resultText").html(result);
        },
        error: function (err) {
            $("#resultText").html(err.statusText);
        }
    });
}