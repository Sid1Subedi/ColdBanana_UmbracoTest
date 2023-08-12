function showAlertDialog({ icon = 'success', title = null, textMsg = null, showCancelButton = false, confirmButtonText = 'OK', confirmButtonColor = '#3085d6', cancelButtonColor = '#d33', } = {}) {
    Swal.fire({
        icon: icon,
        title: title,
        text: textMsg,
        showCancelButton: showCancelButton,
        allowOutsideClick: true,
        allowEscapeKey: false,
        allowEnterKey: true,
        confirmButtonColor: confirmButtonColor,
        cancelButtonColor: cancelButtonColor,
        confirmButtonText: confirmButtonText,
    });
}