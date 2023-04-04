var applicantId = 0, statusAmedment = 0, objApplicant = {}, imagesPath = "", key = "", value = "";

$(document).ready(function () {

    try {
        applicantId = ConvertToInt($("#hfdApplicantId").val());
        statusAmedment = ConvertToInt($("#hfdStatusAmedment").val());

    } catch (e) {
        applicantId = 0;
        statusAmedment = 0;
    }
    if (statusAmedment > 0) {

        $("#ddlStatusType").val(statusAmedment);
    }

    $("#ddlType").val("applicantId");

    key = ConvertToString($("#hfdKey").val());
    if (key.length > 0) {
        $("#ddlType").val(key);
    }

    $(document).on('click', '#btnLoginByDeveloer', function () {

        try {
            var emailIdDeveloper = ConvertToString($("#txtEmailDeveloper").val());
            var resp = CallActionGet("/CommonAdmin/LoginByPhfDeveloper?emailId=" + emailIdDeveloper);
            if (resp.status == true && resp.id > 0) {
                window.open('/ad/applicant-education', '_blank');
                //window.open('/application-status', '_blank');
            }
        } catch (e) {
        }

    });


    $(document).keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            SearchApplicant();
        }
    });


    $(document).on('click', '#btnSearch', function () {

        SearchApplicant();
    });

    $(document).on('click', '#btnUpdateInfo', function () {

        UpdateApplicant();
    });

    $(document).on('click', '#btnSendPassword', function () {

        SendPasswordToApplicant();
    });


    $(document).on('click', '#btnSendAmmendmentEmailSms', function () {

        SendAmendmentEmailSmsToApplicant();
    });


    $(document).on('click', '#ankButtonActivate', function () {

        if (applicantId > 0) {

            var resp = CallActionGet("/CommonAdmin/ApplicantStatusUpdate?applicantId=" + applicantId);
            var key = $("#ddlType").val();
            var value = $("#txtSearch").val();


            var url = window.location.href;
            window.location.href = url;
        }

    });

    $(document).on('click', '#btnUpdateAmmenment', function () {

        UpdateApplicantAmmenmentStatus();
    });


});

function SearchApplicant() {

    var key = $("#ddlType").val();
    var value = ConvertToString($("#txtSearch").val());
    var url = "/admin/applicant-detail?key=" + key + "&value=" + value;


    if (value.length > 0)
        //window.location.href = url;
        window.location.assign(url);
    else { alert("Please enter something which you want to search"); }
}

function SendPasswordToApplicant() {

    var resp = CallActionGet("/CommonAdmin/ApplicantSendMessage?applicantId=" + applicantId);
    if (resp != null && resp.status == true) {
        $("#divSmsSuccess").show();
        $("#divSmsDanger").hide();
    }
    else {
        $("#divSmsSuccess").hide();
        $("#divSmsDanger").show();
    }

}


function SendAmendmentEmailSmsToApplicant() {

    var resp = CallActionGet("/CommonFunctionsAdmin/SendAmendmentEmailSmsAdminByApplicant?applicantId=" + applicantId);
    if (resp != null && resp.status == true) {
        $("#divSmsSuccess").show();
        $("#divSmsDanger").hide();
    }
    else {
        $("#divSmsSuccess").hide();
        $("#divSmsDanger").show();
    }

}

function UpdateApplicant() {

    var errorMsg = "";
    var isError = false;

    $("#divError").hide();
    $("#idErrorMsg").html();

    try {
        var isError = ValidateForm();
        if (isError == false) {

            if (IsExistPMDC() == true) {
                isError = true;
                errorMsg = "<li>This PMDC No already exists. Please try another.</li>"
            }

            if (IsExistEmail() == true) {
                isError = true;
                errorMsg = errorMsg + "<li>This emaild  already exists. Please try another.</li>"
            }

            if (isError == false) {

                var resp = CallActionPost("/CommonAdmin/ApplicantUpdate", objApplicant);
                if (resp != null && resp.status == true) {
                    $("#divError").hide();
                    $("#spnMsg").html("");

                    var url = window.location.href;
                    window.location.href = url;
                }
                else {
                    $("#divError").show();
                    $("#idErrorMsg").html("<li>" + resp.message + "</li>");
                }
            }
            else {
                $("#divError").show();
                $("#idErrorMsg").html(errorMsg);
            }
        }

    } catch (e) {

    }
}

function ValidateForm() {


    $(".req-fld").removeClass("req-fld");


    $("#divError").hide();
    $("#idErrorMsg").html("");

    var isError = false, ctrl = "", errorMessage = "";


    if (applicantId > 0) {
        objApplicant = {};

        objApplicant.applicantId = applicantId;
        objApplicant.name = $("#txtName").val();
        objApplicant.pmdcNo = $("#txtPMDCNo").val();
        objApplicant.emailId = $("#txtEmail").val().toLowerCase();
        objApplicant.contactNumber = $("#txtContactNumber").val();


        if (objApplicant.name == "") {
            $("#txtName").addClass("req-fld");
            isError = true;
            if (ctrl == "")
                ctrl = "txtName";
        }

        if (objApplicant.pmdcNo == "") {
            $("#txtPMDCNo").addClass("req-fld");
            isError = true;
            if (ctrl == "")
                ctrl = "txtPMDCNo";
        }

        if (objApplicant.contactNumber == "") {
            $("#txtContactNumber").addClass("req-fld");
            isError = true;
            if (ctrl == "")
                ctrl = "txtContactNumber";
        }
        else if (objApplicant.contactNumber.length < 11) {

            isError = true;
            $("#txtContactNumber").addClass("req-fld");
            errorMessage = errorMessage + "<li>Please enter valid contact no</li>"
            if (ctrl == "")
                ctrl = "txtContactNumber";
        }

        if (objApplicant.emailId == "") {
            $("#txtEmail").addClass("req-fld");
            isError = true;
            if (ctrl == "")
                ctrl = "txtEmail";
        }
        else {

            if (ValidateEmail(objApplicant.emailId) == false) {
                isError = true;
                errorMessage = errorMessage + "<li>Invalid email id. Please enter valid email id</li>"
            }
        }
    }
    else {

        isError = true;
        errorMessage = errorMessage + "<li>Applicant not exists.</li>"
    }

    if (ctrl != "")
        $("#" + ctrl).focus();

    if (errorMessage.length > 0) {
        $("#divError").show();
        $("#idErrorMsg").html(errorMessage);
    }

    return isError;

}

function IsExistPMDC() {

    var isExist = true;

    try {


        var obj = {};


        obj.id = applicantId;
        obj.search = $("#txtPMDCNo").val();
        obj.condition = "PMDCNO";

        var resp = CallActionPost("/LoggedIn/ApplicantIsExist", obj)
        if (resp != null && resp.id > 0) {
            isExist = true;
        } else {
            isExist = false;
        }

    } catch (e) {

        isExist = true;
    }

    return isExist;
}

function IsExistEmail() {

    var isExist = true;

    try {

        var obj = {};

        obj.id = applicantId;
        obj.search = $("#txtEmail").val();
        obj.condition = "emailId";

        var resp = CallActionPost("/LoggedIn/ApplicantIsExist", obj)
        if (resp != null && resp.id > 0) {
            isExist = true;
        } else {
            isExist = false;
        }

    } catch (e) {

        isExist = true;
    }

    return isExist;
}

function UpdateApplicantAmmenmentStatus() {

    var errorMsg = "";
    var isError = false;

    $("#divError").hide();
    $("#idErrorMsg").html();

    try {

        var isError = false;
        var obj = {};

        obj.applicantId = ConvertToInt($("#hfdApplicantId").val());
        obj.approvalStatusTypeId = 3;
        obj.approvalStatusId = ConvertToInt($("#ddlStatusType").val());
        obj.comments = "";//ConvertToString($("#txtComments").val());

        if (isError == false) {

            var resp = CallActionPost("/VerficationProcess/AddUpdateVerficationAmmenmentStatus", obj);
            if (resp != null && resp.status == true) {
                $("#divError").hide();
                $("#spnMsg").html("");

                location.reload();
            }
            else {
                $("#divError").show();
                $("#idErrorMsg").html("<li>" + resp.message + "</li>");
            }
        }
        else {
            $("#divError").show();
            $("#idErrorMsg").html(errorMsg);
        }


    } catch (e) {

    }
}

