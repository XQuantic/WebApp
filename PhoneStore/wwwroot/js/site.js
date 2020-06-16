$(document).ready(function ()
{
    $("#formCalcPrice").submit(function (e)
    {
        e.preventDefault();
        var data = {
            NameOnePhone: $("#nameOnePhone").val(),
            NameSecondPhone: $("#nameSecondPhone").val()
        };
        $.ajax({
            type: "post",
            contentType: "application/json",
            url: "Home/CalcPrice",
            dataType: "json",
            data: JSON.stringify(data),
            complete: function (response) {
                $("#resPrice").text(response.responseJSON);
            }
        });
    });
});
