$(document).ready(function () {
    $("#formCalcPrice").submit(function (e) {
        e.preventDefault();
        var data = {
            NameOnePhone: $("#nameOnePhone").val(),
            NameSecondPhone: $("#nameSecondPhone").val()
        };
        $.ajax({
            type: "post",
            dataType: "json",
            url: "api/values/calcPrice",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (response) {
                $("#resPrice").text(response);
            },
            error: function (response) {
                if (response.status == 404) $("#resPrice").text(response.responseJSON);
                if (response.responseJSON.errors["NameOnePhone"] != null) $("#nameOnePhone").val(response.responseJSON.errors["NameOnePhone"][0]);
                if (response.responseJSON.errors["NameSecondPhone"] != null) $("#nameSecondPhone").val(response.responseJSON.errors["NameSecondPhone"][0]);
            }
        });
    });
});