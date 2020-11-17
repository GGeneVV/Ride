function shuffle() {
    var from = $('#from').val();
    var to = $('#to').val();
    var duration;
    $.get(`https://dev.virtualearth.net/REST/V1/Routes/Driving?wp.0=${from}&wp.1=${to}&key=Ao0FujyUA1avU6phfkgErqA_GnmNs26KUgvWt6v_HKDXM3wEpZKzn_8j2-LToLbM`).done(function (data) {
        console.log(data);
        duration = data.resourceSets[0].resources[0].travelDuration;
        console.log(duration);
        $('#travelDur').val(duration);
        $.post('https://localhost:5001/Playlists/GeneratePlaylist', $('#form-generate').serialize()).done(function (data) {
            $('#playlist-result').html(data);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == "401" || jqXHR.status == 401) {
                window.location = 'https://localhost:5001/Account/Login';
            }
        });
    });
}

