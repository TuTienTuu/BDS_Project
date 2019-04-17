var category = {
    init: function () {
        category.registerEvents();
    },
    registerEvents: function () {
        //Xử lý cho nút cập nhật trạng thái
        $('.btn-status').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');

            $.ajax({
                url: '/Admin/Menu/ChangeStatus',
                data: {
                    id: id
                },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Hoạt động');
                    }
                    else {
                        btn.text('Tạm khóa');
                    }
                }
            });
        });
    }
}
category.init();