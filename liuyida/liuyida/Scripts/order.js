$(function () {
    

    var form = $("#order-form");
    form.validate({
        errorPlacement: function errorPlacement(error, element) { element.before(error); },
        rules: {
            confirm: {
                equalTo: "#password"
            }
        }
    });
    form.children("div").steps({
        headerTag: "h4",
        bodyTag: "section",
        transitionEffect: "slideLeft",
        onStepChanging: function (event, currentIndex, newIndex) {
            form.validate().settings.ignore = ":disabled,:hidden";
            return form.valid();
        },
        onFinishing: function (event, currentIndex) {
            form.validate().settings.ignore = ":disabled";
            return form.valid();
        },
        onFinished: function (event, currentIndex) {
            $('#order-form').submit();
        }
    });

    $('#CreationTime').datetimepicker({
        formart:'yyyy/MM/dd hh/mm',
        value:new Date()
    });
    
    $('#DeliveryTime').datetimepicker({
        formart: 'yyyy/MM/dd hh/mm',
        value: getDeliveryTime()
    });

    $(".quantity").TouchSpin({
        initval: 0,
        min: 0,
        max: 1000,
        step: 1,
        decimals: 0,
        boostat: 4
    });

    var calculatePrice = function () {
        var pid = $(this).attr('product-id');
        var q = $(this).val();
        var pl = JSON.parse($('#pricelist-' + pid).html());
        console.log(pl);
        var price = 0.0;
        for (i = pl.length - 1; i >= 0; i--) {
            if (q >= pl[i]['quantity']) {
                price = q * pl[i]['price'];
                break;
            }
        }
        $('#price-' + pid).html('$ ' + price.toFixed(2));
        var total = 0.0;
        var priceDom = $('.price').toArray();
        for (i = 0; i < priceDom.length; i++)
        {
            total += parseFloat($(priceDom[i]).html().split(' ')[1]);
        }
        $('#total-price').html('$ ' + total.toFixed(2));
    }

    $('.quantity').each(calculatePrice);
    $('.quantity').change(calculatePrice);
    


    function getDeliveryTime() {
        var t = new Date();
        t.setDate(t.getDate() + 1);
        t.setHours(10);
        t.setMinutes(0);
        return t;
    }
    

});