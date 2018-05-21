$(document).ready(function () {

    //Consulta o webservice/
    $.getJSON("http://localhost:3083/api/venda/relatoriosemanal", function (dados) {

        if (!("erro" in dados)) {
            //Atualiza os campos com os valores da consulta.
            $("#data").val(dados.data);
            $("#total").val(dados.total);
        } //end if.
        else {
            //pesquisa não foi encontrado.
            alert("DADOS NÃO ENCONTRADOS.");
        }
    });

});