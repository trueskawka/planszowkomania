$(document).ready(function(){
  var tableid = getParameterByName("id");
  var tid = tableid;
  tableid = "api/table/details/" + tableid;

  setupSignupButtons(tid);

  authorizedGet(tableid, function(data){
    $("#gameDesc").text(data.game.description);
    $("#gameName").text(data.game.name);
    $("#gameImg").attr("src", "../files/images/" + data.game.image);
    $("#venue").text(data.localizationName);
    $("#time").text(data.eventDate);
    $("#level").text(data.difficulty);
    $("#aggro").text(data.aggresionLevel);

    for (i = 0; i < data.participants.length; i++) {

      if (data.participants[i].status != "Accepted") {
        var html = "";
        html += "<div class='player' data-playerid='" + data.participants[i].id + "'>";
        html += '<div class="text"><img class="playerImg" src="../files/images/' + data.participants[i].image + '" />';
        html += '<h4>'+data.participants[i].userName + '</h4>';
        html += '<div class="buttons"><button class="btn btn-raised btn-warning">Odrzuć</button><button class="btn btn-raised btn-success">Zaakceptuj</button></div>';
        html += '</div></div>';

        $("#pendingInv").append(html);

      } else {
        var html = "";
        html += "<div class='player' data-playerid='" + data.participants[i].id + "'>";
        html += '<div class="text"><img class="playerImg" src="../files/images/' + data.participants[i].image + '" />';
        html += '<h4>'+data.participants[i].userName + '</h4>';
        html += '<div class="buttons"><button class="btn btn-raised btn-warning">Odrzuć</button></div>';
        html += '</div></div>';

        $("#acceptedInv").append(html);
      }
    }
      });

    $("#join").on("click", function(){
      authorizedPost("api/table/join", {tableId:tid}, function(){
        getUserDetails(function(data) {
          var html = "";
          html += "<div class='player' data-playerid='" + data.id + "'>";
          html += '<div class="text"><img class="playerImg" src="../files/images/' + data.image + '" />';
          html += '<h4>'+data.userName + '</h4>';
          html += '<div class="buttons"><button class="btn btn-raised btn-warning">Odrzuć</button><button class="btn btn-raised btn-success">Zaakceptuj</button></div>';
          html += '</div></div>';

          $("#pendingInv").append(html);
          });
    })



});

});

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    url = url.toLowerCase(); // This is just to avoid case sensitiveness
    name = name.replace(/[\[\]]/g, "\\$&").toLowerCase();// This is just to avoid case sensitiveness for query parameter name
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}


var setupSignupButtons = function(tableid) {

  $("#pendingInv").on('click', '.btn-success', function(e) {
    var or = $(e.currentTarget);
    var parent = $(or).parents('.player')[0];
    console.log(tableid);

    var data = {
      "userId": $(parent).data("playerid"),
      "tableid": tableid
    }

    $(parent).remove();

    authorizedPost("api/table/accept", data, function(){
      getUserDetails(function(data) {
          var html = "";
          html += "<div class='player' data-playerid='" + data.id + "'>";
          html += '<div class="text"><img class="playerImg" src="../files/images/' + data.image + '" />';
          html += '<h4>'+data.userName + '</h4>';
          html += '<div class="buttons"><button class="btn btn-raised btn-warning">Odrzuć</button><button class="btn btn-raised btn-success">Zaakceptuj</button></div>';
          html += '</div></div>';

          $("#acceptedInv").append(html);
        });
    });

  $("#pendingInv").on('click', '.btn-warning', function(e) {
    var gr = $(e.currentTarget);
    var parent = $(gr).parents('.player')[0];

    tableid = "api/table/join/" + tableid;

    var data = {
      "userId": $(parent).data("playerid"),
      "tableid": tableid
    }

    authorizedPost("api/table/reject", data, function(){
      getUserDetails(function(data) {
        $(parent).remove();
        });
    });

  });

});
}
