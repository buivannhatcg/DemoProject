

$(document).on('click', '#btn-confirm', showForm);
$(document).on('click', '#btn-close', hideFormConfirm);
$(document).on('click', '#btn-submit', submitTag);

function showForm() {
    if (!validForm())
        return false;


    const tagName = $('#Name').val();
    const displayName = $('#DisplayName').val();

    $('#txt-tag-name-confirm').text(tagName);
    $('#txt-tag-displayname-confirm').text(displayName);
    $('.tag-create').addClass("d-none");
    $('.tag-create-confirm').removeClass("d-none");

}

function hideFormConfirm() {
    $('.tag-create').removeClass("d-none");
    $('.tag-create-confirm').addClass("d-none");
}

function submitTag() {
    $('#frm-tag-create').trigger('submit');
    preventMutipleSubmitForm();
}


function validForm() {
    let flag = true;
    const tagName = $('#Name').val();
    if (!tagName) {
        const required = $('#Name').data("val-required");
        $(".tag-name-error").text(required);
        flag = false;
    } else {
        $(".tag-name-error").text('');
    }
    const displayName = $('DisplayName').val();
    if (!displayName) {
        const required = $('DisplayName').data("val-required");
        $(".tag-dispalyname-error").text(required);
    } else {
        $(".tag-dispalyname-error").text('');
    }

    return flag;
}

function preventMutipleSubmitForm() {
    const $frm = $("#frm-tag-create");
    const $btn = $("#btn-submit");
    if ($frm.valid()) {
        $btn.attr("disabled", true);
        $frm.trigger("submit");
    }
}