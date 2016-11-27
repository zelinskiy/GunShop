var table = $('#allShippingsTable').DataTable({
    searching: false,
    paging: false,
    info: false,
    columns: [
        { orderable: true },
        { orderable: true },
        { orderable: true },
        { orderable: true },
        { orderable: true },
        { orderable: false },
        { orderable: false }
    ]
});