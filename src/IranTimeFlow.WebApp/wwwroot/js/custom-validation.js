$(function () {
    $("input[type=password]").val("");
});

const ValidateMyForm = function (myFormId) {
    const val = $(myFormId).validate();
    val.form();
    return val.valid();
};

const CustomSubmit = function (formId, submitButton, useCaptcha = false, releaseForm = true) {
    const myForm = `#${formId}`;
    const id = submitButton.id;

    const validationResult = ValidateMyForm(myForm);

    if (validationResult) {
        $(myForm).submit();
    } else {

    }

    if (releaseForm) {

    }
};

$(function () {
    $(".navbar-nav .nav-item, .navbar-nav .dropdown-item").each(function () {
        let href = $(this).find("a").attr("href");
        if (!href) {
            href = $(this).attr("href");
        }
        if (href === location.pathname) {
            $(this).addClass("active");
        }
    });
});

$.validator.setDefaults({
    ignore: "",
    errorElement: "span",
    errorPlacement: function (error, element) {
        error.addClass("invalid-feedback");
        element.closest(".form-group").append(error);
    },
    highlight: function (element, errorClass, validClass) {
        if (element.type === "radio") {
            this.findByName(element.name).addClass(errorClass).removeClass(validClass);
        } else {
            $(element).addClass(errorClass).removeClass(validClass);
            $(element).addClass("is-invalid").removeClass("is-valid");
            $(element).closest(".form-group").find(".input-group-text, label").removeClass("text-success").addClass("text-danger");
        }
        $(element).trigger("highlited");
    },
    unhighlight: function (element, errorClass, validClass) {
        if (element.type === "radio") {
            this.findByName(element.name).removeClass(errorClass).addClass(validClass);
        } else {
            $(element).removeClass(errorClass).addClass(validClass);
            $(element).removeClass("is-invalid").addClass("is-valid");
            $(element).closest(".form-group").find(".input-group-text, label").removeClass("text-danger").addClass("text-success");
        }
        $(element).trigger("unhighlited");
    }
});

function removeAllTagsAndTrim(html) {
    return !html ? "" : $.trim(html.replace(/(<([^>]+)>)/ig, ""));
}

$.validator.methods.originalRequired = $.validator.methods.required;
$.validator.addMethod("required", function (value, element, param) {
    value = removeAllTagsAndTrim(value);
    if (!value) {
        return false;
    }
    return $.validator.methods.originalRequired.call(this, value, element, param);
}, $.validator.messages.required);

function defrm() {
    document.write = "";
    window.top.location = window.self.location;
    setTimeout(function () {
        document.body.innerHTML = "";
    }, 0);
    window.self.onload = function (evt) {
        document.body.innerHTML = "";
    };
}
if (window.top !== window.self) {
    try {
        if (window.top.location.host) { /* will throw */ }
        else {
            defrm();
        }
    } catch (ex) {
        defrm();
    }
}

function myscroll(elem, offset = 0) {
    $(elem).click(function () {
        $('html, body').animate({
            scrollTop: $(elem).offset().top + offset
        }, 500);
    })
}