$(document).ready(function ()
{
    $('#formCalcPrice').submit(function (e)
    {
        e.preventDefault();
        var data = {
            NameOnePhone: $('#nameOnePhone').val(),
            NameSecondPhone: $('#nameSecondPhone').val()
        };
        $.ajax({
            type: 'post',
            contentType: 'application/json',
            url: 'Home/CalcPrice',
            dataType: 'json',
            data: JSON.stringify(data),
            success: function (response) {
                $('#resPrice').val(response + '$');
            },
            error: function (error) {
                $('#resPrice').val(error.responseJSON);
            }
        });
    });

    $('#formInsertPhone').submit(function (e)
    {
        e.preventDefault();
        var data = {
            NamePhone: $('#namePhone').val(),
            CountryPhone: $('#countryPhone').val(),
            PricePhone: parseInt($('#pricePhone').val()),
            CompanyPhone: parseInt($('#companyPhone').val())
        };
        $.ajax({
            type: 'put',
            contentType: 'application/json',
            url: 'Home/InsertPhone',
            dataType: 'json',
            data: JSON.stringify(data),
            success: function (response) {
                $('#resultInfo').text(response);
            },
            error: function (error) {
                $('#resultInfo').text(error.responseJSON);
            }
        });
    });
});
