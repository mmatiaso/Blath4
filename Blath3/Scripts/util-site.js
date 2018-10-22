//helpers

var logado = false;


function DataDeHoje() {
    var d = new Date();
    return d.getFullYear() + "-" + d.getMonth() + "-" + d.getDate();
}

function FormataData(dt) {
    var d = new Date(dt);
    return d.getDate() + "/" + d.getMonth() + "/" + d.getFullYear();
}

function Hoje() {
    var d = new Date();
    d = Date(d.getFullYear(), d.getMonth(), d.getDate());
    return d;
}

var map = { "â": "a", "Â": "A", "à": "a", "À": "A", "á": "a", "Á": "A", "ã": "a", "Ã": "A", "ê": "e", "Ê": "E", "è": "e", "È": "E", "é": "e", "É": "E", "î": "i", "Î": "I", "ì": "i", "Ì": "I", "í": "i", "Í": "I", "õ": "o", "Õ": "O", "ô": "o", "Ô": "O", "ò": "o", "Ò": "O", "ó": "o", "Ó": "O", "ü": "u", "Ü": "U", "û": "u", "Û": "U", "ú": "u", "Ú": "U", "ù": "u", "Ù": "U", "ç": "c", "Ç": "C", " ":"-" };

function removerAcentos(s) { return s.replace(/[\W\[\] ]/g, function (a) { return map[a] || a }) };



function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};

function getUrlLastSlash() {
    var pathArray = window.location.pathname.split('/');
    return pathArray[pathArray.length-1];
}


function formataDinheiro(n) {
    if (n == null) {
        n = 0;
    } 
    return "R$ " + n.toFixed(2).replace('.', ',').replace(/(\d)(?=(\d{3})+\,)/g, "$1.");
}

function formataPorcentagem(n) {
    if (n == null) {
        n = 0;
    } 
    return n.toFixed(0).replace('.', ',').replace(/(\d)(?=(\d{3})+\,)/g, "$1.") + "%";
}

function formataDec2(n) {
    if (n == null) {
        n = 0;
    } 
    return n.toFixed(2).replace('.', ',').replace(/(\d)(?=(\d{3})+\,)/g, "$1.");
}

function unicodeEscape(str) {
    return str.replace(/[\s\S]/g, function (escape) {
        return '\\u' + ('0000' + escape.charCodeAt().toString(16)).slice(-4);
    });
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function writeCookie(name, value, days) {
    var date, expires;
    if (days) {
        date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}


function IsRegistered(param) {
    var _uid = getCookie("uid");
    var retorno = null;
    //alert(_uid);
    if (_uid != null) {
        //check if exists
        
        var urlcall = "/api/islogged?uid=" + _uid;
        var jqxhr = $.ajax(urlcall)
            .done(function (data) {
                //retorno = true;
                logado = true;
                //return retorno;
                console.log(logado);
                //callback(logado);
            });
    }
    else {
        //
    }
    
}



