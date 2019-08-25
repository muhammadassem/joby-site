$(document).bind("ajaxStart", function () {
    $("#spinner-loader").removeClass("invisible");
}).bind("ajaxStop", function () {
    $("#spinner-loader").addClass("invisible");
});

function showImagePreview(imageUploader) {
    if (imageUploader.files && imageUploader.files[0]) {
        $("#error").empty(); // clear error

        var fileReader = new FileReader();

        fileReader.onload = function (e) {
            var previewImage = document.getElementById('imagePreview');
            $(previewImage).attr('src', e.target.result);
        }

        fileReader.readAsDataURL(imageUploader.files[0]);
    }
}

function postJob(form) {

    if ($("#jobId").attr("data-jobId") == 0) {
        // Add New Job

        if ($("#file").val() == "") {
            $("#error").empty().append("Image is Required");
            return false;
        }
    }

    var inputFile = document.getElementById("file");
    fileUpload(inputFile);

    $.validator.unobtrusive.parse(form);

    if ($(form).valid()) {

        var data = new FormData(form);

        var ajaxConfig = {
            url: form.action,
            type: "POST",
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (response) {
                $("#viewJobsTab").html(response); // asyncrounsly render this view

                //if (typeof activateJobsTable !== 'undefined' && $.isFunction(activateJobsTable)) {
                //    activateJobsTable();
                //    console.log("table");
                //}

                //refreshTab($(form).attr('data-resetUrl'), "#JobTab");
                //renameTabTitle("New Category", "#category-config-tab");
                //$("#notification").notify("Added Successfully", "success");
                //redirectToTab(0);
            },
            error: function () {
                alert("error");
            }
        };

        //if ($(form).attr('enctype') == "multipart/form-data") {
        //    // add these two properties to ajax object

        //    ajaxConfig["contentType"] = false;
        //    ajaxConfig["processData"] = false;
        //}

        $.ajax(ajaxConfig);
    }

    return false; // to prevent submition
}

function postCategory(form) {

    //event.preventDefault();

    $.validator.unobtrusive.parse(form);
    console.log(form.action);

    if ($(form).valid()) {

        var ajaxConfiguration = {
            url: form.action,
            type: "POST",
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (response) {
                $("#viewCategoriesTab").html(response); // asyncrounsly render this view
                refreshTab($(form).attr('data-resetUrl'), "#categoryTab");
                renameTabTitle("New Category", "#category-config-tab");

                if (typeof activateJobCategoriesTable !== 'undefined' && $.isFunction(activateJobCategoriesTable)) {
                    activateJobCategoriesTable();
                }

                $("#notification").notify("Added Successfully", "success");
                redirectToTab(0);
            },
            error: function (response) {
                alert("error");
            }
        };

        $.ajax(ajaxConfiguration);
    }

    return false;
}

function fileUpload(inputFile) {

    if (inputFile.files.length > 0) {
        // check file size in Mb
        var fileSize = inputFile.files[0].size / 1024 / 1024;


        if (fileSize > 2) {
            $("#error").empty().append("file is too large");
            return false;
        }

        // check file extension
        var supportedFileExtensions = ["png", "jpg", "BMP", "gif"]
        var fileExtension = (inputFile.value).substr(inputFile.value.lastIndexOf(".") + 1);

        // or can use $.inArray()
        if (!supportedFileExtensions.find(x => x == fileExtension)) {
            $("#error").empty().append("file is not supported");
            return false;
        }
    }
}
function refreshTab(url, tabId) {

    $.ajax({
        url: url,
        method: "GET",
        success: function (response) {
            $(tabId).html(response);
        }
    });

    renameTabTitle("New Job", "#job-config-tab");
}


function redirectToEditJobAction(url, idOfTab) {

    $.ajax({
        url: url,
        method: "GET",
        success: function (response) {
            $(idOfTab).html(response);
            renameTabTitle("Edit Job", "#job-config-tab");
            redirectToTab(1);
        },
        error: function (jqxhr, status, exception) {
            alert('Exception:', exception);
        }
    });

    return false;
}

function redirectToEditCategoryAction(url, idOfTab) {

    $.ajax({
        url: url,
        method: "GET",
        success: function (response) {
            $(idOfTab).html(response);
            renameTabTitle("Edit Category", "#category-config-tab");
            redirectToTab(1);
        },
        error: function (jqxhr, status, exception) {
            alert('Exception:', exception);
        }
    });

    return false;
}

function renameTabTitle(tabTitle, tabId) {
    $(tabId).html(tabTitle);
}

function redirectToTab(tabNumber) {
    $(".nav.nav-tabs li a:eq(" + tabNumber + ")").tab("show");
}

function redirectToAddJobAction(url, idOfTab) {

    $.ajax({
        url: url,
        method: "GET",
        success: function (response) {
            $(idOfTab).html(response);
            renameTabTitle("Add New Job", "#job-config-tab");
            redirectToTab(1);
        },
        error: function (jqxhr, status, exception) {
            alert('Exception:', exception);
        }
    });
}


function deleteJob(object) {

    var id = $(object).attr("data-job-id");

    if (confirm("Are you sure to delete")) {
        $.ajax({
            url: "/Job/Delete/" + id,
            method: "POST",
            success: function (response) {
                $("#viewJobsTab").html(response);

                if (typeof activateJobsTable !== 'undefined' && $.isFunction(activateJobsTable)) {
                    activateJobsTable();
                }
            },
            error: function () {
                alert("failed");
            }
        });
    }
    return false;
}

function deleteCategory(category) {

    if (confirm("Are You Sure?")) {
        var id = $(category).attr("data-category-id");

        $.ajax({
            type: "POST",
            url: '/JobCategory/Delete/' + id,
            success: function (response) {

                $("#viewCategoriesTab").html(response);

                if (typeof activateJobCategoriesTable !== 'undefined' && $.isFunction(activateJobCategoriesTable)) {
                    activateJobCategoriesTable();
                }
                
            },
            error: function () {
                console.log("error");
            }
        });
    }

    return false;
}
