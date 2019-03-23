$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });

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
                });

                var tableElement = $('#disciplines');
                var tableUrl = tableElement.data('url');
                $.get(tableUrl).done(function (table) {
                    tableElement.replaceWith(table);
                });

                placeholderElement.find('.modal').modal('hide');            
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