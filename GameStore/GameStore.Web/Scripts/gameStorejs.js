$('#filteredGames')
    .each(function () {
        $(this).data('serialized', $(this).serialize())
    })
    .on('change input', function () {
        $(this)
            .find('#filter')
                .attr('disabled', $(this).serialize() === $(this).data('serialized'))
            ;
    })
    .find('#filter')
    .attr('disabled', true);

$('#filteredGames').find("#page").attr('disabled', false);

