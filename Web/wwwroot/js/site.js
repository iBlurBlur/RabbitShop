$(document).ready(function () {
    // Show loading indicator when page is first loaded
    $("#loadingIndicator").addClass('d-flex');

    // Hide loading indicator when page is fully loaded
    $(window).on("load", function () {
        hideLoadingIndicator()
    });

    // Show loading indicator during AJAX requests
    $(document).ajaxStart(function () {
        hideLoadingIndicator()
    });

    // Hide loading indicator when AJAX requests are finished
    $(document).ajaxStop(function () {
        hideLoadingIndicator()
    });
});

async function hideLoadingIndicator() {
   await delay(1000);
   $("#loadingIndicator").addClass('d-none');
}

function delay(time) {
    return new Promise(function (resolve) {
        setTimeout(resolve, time);
    });
}

function showToast(isSuccess, message, width) {
    let toastMessage = message
    let textColor = 'text-danger'
    let backgroundColor = 'rgb(253 232 232)'
    if (isSuccess) {
        toastMessage = toastMessage ?? 'Successfully';
        textColor = 'text-success'
        backgroundColor = 'rgb(222 247 236)'
    } else {
        toastMessage = toastMessage ?? 'Something went wrong';
    }

    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        timer: 3000,
        showConfirmButton: false,
        timerProgressBar: false,
        showCloseButton: true,
    })

    Toast.fire({
        width: width ?? 400,
        html: `<div class="${textColor}"> <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-info-circle-fill" viewBox="0 0 16 16"> <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2z"/> </svg> <span class="mx-2">${message}</span></div>`,
        background: backgroundColor
    })
}

function previewFile(input) {
    let file = input.files[0];
    let mixedfile = file['type'].split("/");
    let filetype = mixedfile[0];
    let previewImage = $("#preview_image")
    let videoPlayer = $("#video_player")
    let previewVideo = $("#preview_video")

    previewImage.addClass("d-none")
    videoPlayer.addClass("d-none")

    previewImage.attr("src", "");
    previewVideo.attr("src", "");

    switch (filetype) {
        case "image":
            let reader = new FileReader();
            reader.onload = function (e) {
                previewImage.removeClass("d-none")
                previewImage.addClass("w-100")
                previewImage.attr("src", e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
            break;
        case "video":
            videoPlayer.removeClass("d-none")
            previewVideo.attr("src", URL.createObjectURL(input.files[0]));
            videoPlayer[0].load();
            break;
        case "application":
            // pdf, excel, ...
            break;
        default:
            alert("Invalid file type")
            break;
    }
}

async function confirmDelete(event) {
    event.preventDefault();

    const result = await Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#ff9800',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    })

    let isConfirmed = result.isConfirmed;
    if (isConfirmed) {
        window.location = event.target.href;
    }

    return isConfirmed;
} 