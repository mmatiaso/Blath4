

$(document).ready(function () {

    //_layout ------------------------------------------------------------------------------

    //init - UF e Cidade
    var initUF = "RJ";
    var initUFid = 33;
    var initCid = "Rio de Janeiro";

    //cookies
    var _getCookieUF = getCookie("cuf");
    var _getCookieUFid = getCookie("cufid");
    var _getCookieCidade = getCookie("ccidade");
    if (_getCookieUF != "" && _getCookieUF != null) { //se tem cookie UF
        initUF = _getCookieUF;
        initUFid = _getCookieUFid;
    } else {
        writeCookie("cufid", initUFid, 360);
    }
    if (_getCookieCidade != "" && _getCookieCidade != null) { //se tem cookie Cidade
        initCid = _getCookieCidade;
    } else {
        writeCookie("ccidade", initCid, 360);
    }
    

    //Cria o selects de UF e Cidade
    GetUFs(initUF);
    GetCidades(initUFid, initCid);

    $("#usuarioUF").change(function () {
        var rUF = $("#usuarioUF").val();
        GetCidades(rUF);
        writeCookie("cufid", rUF, 360);
    })

    $("#usuarioCidade").change(function () {
        SelUFCid();
        location.reload();
    })

    //UF e Cid Sel
    function SelUFCid() {
        var ufName = $("#usuarioUF option:selected").text();
        $("#ufSel").text(ufName);
        writeCookie("cuf", ufName, 360);
        var cidName = $("#usuarioCidade").val();
        $("#cidSel").text(cidName);
        writeCookie("ccidade", cidName, 360);
    }

    //GetUFs
    function GetUFs(uf) {
        var urlReq = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
        var ahtml = "";

        var jqxhr = $.get(urlReq, function () {

        })
            .done(function (data) {
                //sort
                var byUf = data.slice(0);
                byUf.sort(function (a, b) {
                    var x = a.sigla;
                    var y = b.sigla;
                    return x < y ? -1 : x > y ? 1 : 0;
                });
                //preenchendo o select

                for (i = 0; i < byUf.length; i++) {
                    if (uf == byUf[i].sigla) {
                        ahtml += "<option value=" + byUf[i].id + " selected>" + byUf[i].sigla + "</option>";
                    } else {
                        ahtml += "<option value=" + byUf[i].id + ">" + byUf[i].sigla + "</option>";
                    }

                }
                $("#usuarioUF").append(ahtml);
            })
            .fail(function () {
                //alert("O CEP informado não existe.");

            })
            .always(function () {

            });
    }

    //Get Cidades
    function GetCidades(UFId, cid) {
        var urlReq = "http://servicodados.ibge.gov.br/api/v1/localidades/estados/" + UFId + "/municipios";
        var ahtml = "";
        //$("#areaAtuacaoCidade").val("");
        $('#usuarioCidade').children('option').remove();
        if (UFId == "0") {
            //ahtml = "<option value='0'>Todo o Brasil</option>";
            //$("#areaAtuacaoCidade").append(ahtml);
        }
        else {
            ahtml = "<option value='0'>Selecione a cidade</option>";
            var jqxhr = $.get(urlReq, function () {

            })
                .done(function (data) {
                    //sort
                    var byCid = data.slice(0);
                    byCid.sort(function (a, b) {
                        var x = a.nome;
                        var y = b.nome;
                        return x < y ? -1 : x > y ? 1 : 0;
                    });
                    //preenchendo o select

                    for (i = 0; i < byCid.length; i++) {
                        //console.log(data[i].sigla);          
                        //ahtml += "<option value=" + byCid[i].id + ">" + byCid[i].nome + "</option>";
                        if (cid == byCid[i].nome) {
                            ahtml += "<option selected>" + byCid[i].nome + "</option>";
                        } else {
                            ahtml += "<option>" + byCid[i].nome + "</option>";
                        }

                    }
                    $("#usuarioCidade").append(ahtml);
                })
                .fail(function () {
                    //alert("O CEP informado não existe.");

                })
                .always(function () {

                });
        }


    }


});






