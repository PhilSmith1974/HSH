<script type="text/javascript">
    $(function(){
        $("#myDiv").AutoAddress({
            key: "YOUR_KEY",
            vanityMode: true,
            addressProfile: "Default5Line",
            onAddressFound: function (data) {

                if (data.reformattedAddress) {
                    $.each(data.reformattedAddress, function (index, value) {
                        $('#addressLine' + (index + 1)).val(value);
                    });
                }
                if (data.postcode) {
                    $('#postcode').val(data.postcode);
                }
            }
        });
    });
</script>