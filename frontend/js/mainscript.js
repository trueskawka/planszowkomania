$( document ).ready(function() {

  getUserDetails(function(data) {
    $('#name').text(data.userName);
  });

  setupSignupButtons();

  fetchUserTables();

});
// Ustawianie buttonów do zapisywania się do stołu
var setupSignupButtons = function() {

  $(".rooms").on('click', '.btn-warning', function(e) {
    var or = $(e.currentTarget);
    var parent = $(or).parents('.boxy')[0];
    var gr = $(parent).find('.btn-success');

    var data = { "tableId": $(parent).data("tableid") };

    authorizedPost("api/table/join", data, function(){
      getUserDetails(function(data) {
        var imurl = "../files/images/" + data.image;
        var empty = $(parent).find(".empty-user").first()[0];
        $(empty).css("background-image", "url("+imurl+")");
        });
    });

    or.addClass("hidden");
    gr.removeClass("hidden");
  });

  $(".rooms").on('click', '.btn-success', function(e) {
    var gr = $(e.currentTarget);
    var parent = $(gr).parents('.boxy')[0];
    var or = $(parent).find('.btn-warning');

    or.removeClass("hidden");
    gr.addClass("hidden");
  });

}

// Pobieranie wszystkich stołów
var fetchUserTables = function() {

  $.getJSON(serviceBase + 'api/table/all', function(data) {

    for (i = 0; i < data.length; i++) {
      console.log(data[i]);

      var html = "";
      html += "<div class='boxy' data-tableid='" + data[i].id + "'>";
      html += '<div class="game-content"><img src="../files/images/' + data[i].game.image + '"/>';
      html += '<div class="text-content"><div class="btn btn-warning btn-fab"><i class="fa fa-user-plus"></i></div><div class="btn btn-success btn-fab hidden"><i class="fa fa-check"></i></div><h4>' + data[i].game.name + '</h4><table>';
      html += '<tr><td class="lead-cl">Miejsce: </td><td>' + data[i].localizationName + '</td></tr>';
      html += '<tr><td class="lead-cl">Termin: </td><td>' + data[i].eventDate + '</td></tr>';
      html += '<tr><td class="lead-cl">Poziom gry: </td><td>' + data[i].difficulty + '</td></tr>';
      html += '<tr><td class="lead-cl">Poziom agresji: </td><td>' + data[i].aggresionLevel + '</td></tr></table>';
      html += '<div class="users">';

      for (j = 0; j < data[i].participants.length; j++) {
          html+= '<div class="user"><img src="../files/images/' + data[i].participants[j].image + '" class="usrimg" / ></div>';
      }

      if (data[i].participants.length < data[i].usersRequired) {
        for (k = 0; k < data[i].usersRequired - data[i].participants.length; k++) {
          html += '<div class="empty-user"></div>';
        }
      }

      html += '</div></div></div></div>';

      $(".rooms").append(html);
    }

  })
}
