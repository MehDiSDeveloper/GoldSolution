
$(document).ready(function () {
    $(document).on("click", ".ExecuteSysSellBillButton", function () {
        var id = $(this).attr('myVal');
        $.ajax({
            type: 'POST',
            url: '/executesyssellbill',
            data: { id: id },
            cache: false,
            success: function (response) {
                var status = '';
                var Status = $("input[type='radio'][name='status']:checked");
                if (Status.length > 0) {
                    status = Status.val();
                }
                var type = "";
                var Type = $("input[type='radio'][name='type']:checked");
                if (Type.length > 0) {
                    type = Type.val();
                }
                var searchString = "";
                var searchString = $('#searchString').val();
                $.ajax({
                    data: {
                        status: status,
                        type: type,
                        searchString: searchString
                    },
                    url: '/SystemGoldBillsListPartial',
                    cache: false,
                    success: function () {
                        $('#ajax').load(document.URL + ' #ajax2');
                        alert(response);
                    },
                    error: function () {
                        alert('خطایی رخ داده است');
                    } 
                });
            }
        });
    });
    $(document).on("click", ".ExecuteSysBuyBillButton",function () {
        var id = $(this).attr('myVal');
        $.ajax({
            type: 'POST',
            url: '/executesysbuybill',
            data: { id: id },
            cache: false,
            success: function (response) {
                var status = '';
                var Status = $("input[type='radio'][name='status']:checked");
                if (Status.length > 0) {
                    status = Status.val();
                }
                var type = "";
                var Type = $("input[type='radio'][name='type']:checked");
                if (Type.length > 0) {
                    type = Type.val();
                }
                var searchString = "";
                var searchString = $('#searchString').val();
                $.ajax({
                    data: {
                        status: status,
                        type: type,
                        searchString: searchString
                    },
                    url: '/SystemGoldBillsListPartial',
                    cache: false,
                    success: function () {
                        $('#ajax').load(document.URL + ' #ajax2');
                        alert(response);
                    },
                    error: function () {
                        alert('خطایی رخ داده است');
                    }
                });
            }
        });
    });




    $(document).on("click", ".switchSuspendButton", function () {
        var id = $(this).attr('idVal');
        var billType = $(this).attr('billTypeVal');
        $.ajax({
            type: 'POST',
            url: '/switchsusspendbill',
            data: {
                id: id,
                billType: billType
            },
            cache: false,
            success: function (response) {
                var status = '';
                var Status = $("input[type='radio'][name='status']:checked");
                if (Status.length > 0) {
                    status = Status.val();
                }
                var type = "";
                var Type = $("input[type='radio'][name='type']:checked");
                if (Type.length > 0) {
                    type = Type.val();
                }
                var searchString = "";
                var searchString = $('#searchString').val();
                $.ajax({
                    data: {
                        status: status,
                        type: type,
                        searchString: searchString
                    },
                    url: "/SystemGoldBillsListPartial",
                    cache: false,
                    success: function () {
                        $('#ajax').load(document.URL + ' #ajax2');
                        alert(response);
                    },
                    error: function () {
                        alert(' (خطایی رخ داده است) ' + response);
                    }
                });
            }
        });
    });
})