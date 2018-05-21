
$('#filteredGames')
    .each(function () {
        $(this).data('serialized', $(this).serialize())
    })
    .on('change input', function () {
        $(this)
                .find('input:submit')
                .attr('disabled', $(this).serialize() === $(this).data('serialized'))
            ;
    })
    .find('input:submit')
    .attr('disabled', true);

$("#notDisabled").find('input: submit, button: submit')
    .attr('disabled', false);
