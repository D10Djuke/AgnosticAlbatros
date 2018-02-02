function AjaxGetLoginModal() {
    return $.ajax({
        type: 'POST',
        url: '/Modal' + '/GetLoginModal',
        dataType: 'html',
        error: function (jqXHR, exception) {
            console.log(jqXHR);
            console.log(exception);
        }
    });
}