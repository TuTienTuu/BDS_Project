$(function () {
});

function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        type: type ? type : 'GET',
        data: data,
        contentType: 'application/json'
    });
};
//Here $(this)$(function () {
//});  

AjaxCall('/Admin/Menu/GetLevelMenu', null).done(function (response) {
    if (response.length > 0) {
        $('#LevelMenu').html('');
        var options = '';
        //options += '<option value="0">Select</option>';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].Value + '">' + response[i].Text + '</option>';
        }
        $('#LevelMenu').append(options);
    }
}).fail(function (error) {
    alert(error.StatusText);
});


$('#LevelMenu').on("change", function () {
    var country = $('#LevelMenu').val();

    var obj = { country: country };
    //alert(obj.country);
    AjaxCall('/Admin/Menu/GetParentID/' + obj.country + '', JSON.stringify(obj), 'POST').done(function (response) {
        if (response) {
            //$('#stateDropDownList').addClass('form-control');
            $('#ParentID').html('');
            var options = '';
            //options += '<option value="Select">Select</option>';
            for (var i = 0; i < response.length; i++) {
                options += '<option value="' + JSON.stringify(response[i].ID) + '">' + JSON.stringify(response[i].MenuName_VN) + '</option>';
            }
            $('#ParentID').append(options);

        }
    }).fail(function (error) {
        alert(error.StatusText);
    });
});

$('#ParentID').on('change', function () {
    var parentId = $('#ParentID').val();
    var id = $('#parentId').attr('value', parentId);
});

//$(function () {
//    var ddlCustomers = $("#stateDropDownList");
//    alert($(this).data);
//    ddlCustomers.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
//    $.ajax({
//        type: "POST",
//        url: "/Brand/AjaxMethod",
//        data: '{}',
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
//            $.each(response, function () {
//                ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
//            });
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//});
