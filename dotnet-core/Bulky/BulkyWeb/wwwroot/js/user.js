let dataTable

const loadDataTable = () => {
    dataTable = $('#table-data').DataTable({
        ajax: {
            url: '/admin/user/getall'
        },
        columns: [
            {
                data: 'name',
                width: '15%'
            },
            {
                data: 'email',
                width: '15%'
            },
            {
                data: 'phoneNumber',
                width: '15%'
            },
            {
                data: 'company.name',
                width: '15%'
            },
            {
                data: 'role',
                width: '15%'
            },
            {
                data: { id: 'id', lockoutEnd: 'lockoutEnd' },
                render: (data) => {
                    const today = new Date().getTime();
                    const lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return `
                        <div class="w-75 btn-group">
                            <a onClick="LockUnlock('${data.id}')" class="btn btn-success mx-2" style="cursor: pointer; width: 100px">
                                <i class="bi bi-lock"></i> Lock
                            </a>
                            <a href="/Admin/User/Rolemanagement?userId=${data.id}" class="btn btn-success mx-2" style="cursor: pointer; width: 150px">
                                <i class="bi bi-person-fill-lock"></i> Permission
                            </a>
                        </div>
                        `
                    } else {
                        return `
                        <div class="w-75 btn-group">
                            <a onClick="LockUnlock('${data.id}')" class="btn btn-success mx-2" style="cursor: pointer; width: 100px">
                                <i class="bi bi-unlock"></i> Unlock
                            </a>
                            <a href="/Admin/User/Rolemanagement?userId=${data.id}" class="btn btn-success mx-2"  style="cursor: pointer; width: 150px">
                                <i class="bi bi-person-fill-lock"></i> Permission
                            </a>
                        </div>
                        `
                    }
                },
                width: '25%'
            },
            ]
        })
}

$(document).ready(() => {
    loadDataTable()
})

function LockUnlock(id) {
    $.ajax({
        type: 'POST',
        url: '/admin/user/lockunlock',
        data: JSON.stringify(id),
        contentType: 'application/json',
        success: (data) => {
            if (data.success) {
                toastr.success(data.message)
                dataTable.ajax.reload()
            }
        }
    })
}