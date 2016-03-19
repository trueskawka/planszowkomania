$(document).ready(function(){
  var tableid = getParameterByName("id");
  var tid = tableid;
  tableid = "api/table/details/" + tableid;

  authorizedGet(tableid, function(data){
    console.log(data);

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
          html += '<div class="buttons"><button class="btn btn-raised btn-warning">Odrzuć</button></div><button class="btn btn-raised btn-success">Zaakceptuj</button>';
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
