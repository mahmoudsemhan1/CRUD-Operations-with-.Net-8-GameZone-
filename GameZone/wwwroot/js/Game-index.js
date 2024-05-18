$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);
        var id = btn.data('id'); // Assuming the data-id attribute holds the game's ID

        const   swal = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger mx-2",
                cancelButton: "btn btn-light"
            },
            buttonsStyling: false
        });

        swal.fire({
            title: "Are you sure that you need to ddelete this game?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Games/Delete/${id}`,
                    method: 'DELETE',
                    success: function () {
                        swal.fire(
                             'Deleted!',
                             'Game has been deleted.',
                             'success'
                        );
                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        swal.fire(
                            'Ooops...',
                            'Something went wrong',
                            'error'
                        );
                    }
                });
              
            }
        });
       
    });
});
