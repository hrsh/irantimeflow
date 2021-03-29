$(() => {

});

showLoader = (container = "") => {

}

hideLoader = (container = "") => {

}

lockButton = (id) => {
    $(`#${id}`).attr("disabled", true);
}

unlockButton = (id) => {
    $(`#${id}`).attr("disabled", false);
}