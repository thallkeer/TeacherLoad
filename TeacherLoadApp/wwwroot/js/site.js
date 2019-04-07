$(document).on('click', 'button[data-toggle="modal"]',function (event) {
    var placeholderElement = $('#add-discipline');
    //$('button[data-toggle="modal"]').click(function (event) {
        event.preventDefault();
        var url = $(this).data('url');        
        $.get(url).done(function (data) {           
            $('#dialog-content').html(data);
            placeholderElement.modal('show');
        });        
    //});

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                var notificationsPlaceholder = $('#notification');
                var notificationsUrl = notificationsPlaceholder.data('url');
                $.get(notificationsUrl).done(function (notifications) {                    
                    notificationsPlaceholder.html(notifications);
                    notificationsPlaceholder.show('slow');
                    setTimeout(function () { notificationsPlaceholder.hide('slow'); }, 2000);
                });

                var tableElement = $('#disciplines');
                var tableUrl = tableElement.data('url');
                $.get(tableUrl).done(function (table) {
                    tableElement.replaceWith(table);
                });

                //placeholderElement./*find('.modal').*/modal('hide');
            }
        });
    });
});

function getGroupsByCourse(route, year) {
    $.ajax({
        type: 'GET',
        url: route,
        data: { studyYear: year },
        success: function (data) {
            $('.groups').replaceWith(data);
        }
    });
};

function getDisciplinesByGroup(route,group) {
    $(".selected-group").change(function () {
        $.ajax({
            type: 'GET',
            url: route,
            data: { id: group },
            success: function (data) {
                $('#content').html(data);
            }
        });
    });
}

function calculate(route) {
    $.ajax({
        type: 'GET',
        url: route,
        data: { classID: $("#classType").val(), count: $("#studentCount").val() },
        success: function (data) {
            $('#totalVolume').val(data);
        }
    });
};