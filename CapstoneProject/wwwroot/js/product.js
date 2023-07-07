// DataTable to replace the index table for product
// Use the admin/product/getall endpoint to get the JSON for the data column names
// Must be exact, case sensitive
var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable()
{
    dataTable = $('#myTable').DataTable(
        {
            "ajax":
            {
                url: '/admin/product/getall'
            },
            "columns":
                [
                    { data: 'name', "width": "20%" },
                    { data: 'productCode', "width": "15%" },
                    { data: 'netWeight', "width": "5%" },
                    { data: 'grossWeight', "width": "5%" },
                    { data: 'price', "width": "5%" },
                    { data: 'bulkRate10', "width": "5%" },
                    { data: 'bulkRate100', "width": "5%" },
                    { data: 'inventory', "width": "5%" },
                    { data: 'customer.name', "width": "10%" },
                    {
                        data: 'id',
                        "render": function (data) {
                            return `<div class="w-75 btn-group" role="group">
                            <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>  
                            <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-dash-circle"></i> Delete</a>
                            </div>`
                        },
                        "width" : "25%"
                    }
                ]
        }
    );
}
// SweetAlert2 popup
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    // Page is not refreshed after deletion, ajax request to refresh
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}

