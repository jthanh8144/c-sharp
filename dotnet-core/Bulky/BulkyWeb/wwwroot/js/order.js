let dataTable

const loadDataTable = (status) => {
    dataTable = $('#table-data').DataTable({
        ajax: {
            url: '/admin/order/getall?status=' + status
        },
        columns: [
            {
                data: 'id',
                width: '5%'
            },
            {
                data: 'name',
                width: '25%'
            },
            {
                data: 'phoneNumber',
                width: '15%'
            },
            {
                data: 'applicationUser.email',
                width: '15%'
            },
            {
                data: 'orderStatus',
                width: '15%'
            },
            {
                data: 'orderTotal',
                width: '10%'
            },
            {
                data: 'id',
                render: (id) => `
                <div class="w-75 btn-group">
                    <a href="/Admin/Order/Details?orderId=${id}" class="btn btn-success mx-2">
                        <i class="bi bi-eye"></i>
                    </a>
                </div>
                `,
                width: '5%'
            },
            ]
        })
}

$(document).ready(() => {
    const url = window.location.search
    console.log(url)
    const status = url.includes("=") ? url.split("=")[1] : ""
    loadDataTable(status)
})
