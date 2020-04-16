$(document).ready(function ()
{
    $('#formCalcPrice').submit(function (e)
    {
        e.preventDefault();
        let data = {};
        data["NameOnePhone"] = $('#nameOnePhone').val();
        data["NameSecondPhone"] = $('#nameSecondPhone').val();
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
        let data = {};
        data["NamePhone"] = $('#namePhone').val();
        data["CountryPhone"] = $('#countryPhone').val();
        data["PricePhone"] = $('#pricePhone').val();
        data["CompanyPhone"] = $('#companyPhone').val();
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
