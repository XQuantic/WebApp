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
            url: "api/phone/calcPrice",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (response) {
                $("#resPrice").text(response + "$");
            },
            error: function (response) {
                validate(response);
            }
        });
    });
});

function validate(data) {
    if (data.status == 404) $("#resPrice").text("Data not found");
    if (data.responseJSON.errors["NameOnePhone"] != null) $("#nameOnePhone").val(data.responseJSON.errors["NameOnePhone"]);
    if (data.responseJSON.errors["NameSecondPhone"] != null) $("#nameSecondPhone").val(data.responseJSON.errors["NameSecondPhone"]);
}