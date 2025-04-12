let dataTable

const loadDataTable = () => {
    dataTable = $('#table-data').DataTable({
        ajax: {
            url: '/admin/company/getall'
        },
        columns: [
            {
                data: 'name',
                width: '15%'
            },
            {
                data: 'streetAddress',
                width: '15%'
            },
            {
                data: 'city',
                width: '15%'
            },
            {
                data: 'state',
                width: '15%'
            },
            {
                data: 'postalCode',
                width: '15%'
            },
            {
                data: 'phoneNumber',
                width: '15%'
            },
            {
                data: 'id',
                render: (id) => `
                <div class="w-75 btn-group">
                    <a href="/Admin/Company/Upsert?id=${id}" class="btn btn-success mx-2">
                        <i class="bi bi-pencil-square"></i> Edit
                    </a>
                    <a onClick="handleDelete('/admin/company/delete/${id}')" class="btn btn-danger mx-2">
                        <i class="bi bi-trash"></i> Delete
                    </a>
                </div>
                `,
                width: '15%'
            },
            ]
        })
}

const handleDelete = (url) => {
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
                url,
                type: 'DELETE',
                success: (data) => {
                    if (data.success) {
                        dataTable.ajax.reload()
                        toastr.success(data.message)
                    } else {
                        toastr.error(data.message)
                    }
                }
                })
        }
    })
}

$(document).ready(() => {
    loadDataTable()
})
