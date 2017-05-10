

dragula($('.bucket').toArray());

$('#toggle-details').mouseup(function () {
    if ($(this).attr('status') == 0) {
        $(this).attr('status', 1);
        $(this).html('Hide orders');
        $('.bucket').show();
    }
    else {
        $(this).attr('status', 0);
        $(this).html('Show orders');
        $('.bucket').hide();
    }
});