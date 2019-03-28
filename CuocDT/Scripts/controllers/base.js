var common = {
    init: function () {
        common.registerEvent();
    },

    registerEvent: function () {
        $('#btnCreateLog').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Log/GhiLog',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/success-ghilog";
                    }
                }
            });
        });

        $("#formsubmit").submit(function (e) {
            e.preventDefault(); // avoid to execute the actual submit of the form.
            var form = $(this);
            $.ajax({
                url: '/Cuoc/CheckSDT',
                dataType: 'json',
                data: form.serialize(),
                type: 'POST',
                success: function (res) {
                    if (res.status == false) {
                        alert("Số điện thoại bạn nhập không chính xác");
                    }
                    else {
                        var url = '/tim-kiem?sdt=' + res.val;
                        window.location.href = url;
                    }
                }
            });
        });
    }
}

common.init();