$(document).ready(function () {
    $('#lista_produtos').DataTable({
        //"ajax": 'http://localhost:2996/public_api/ListaProdutos',
        //"columns": [
        //    { "data": "id" },
        //    { "data": "nome" },
        //    { "data": "quantidade" },
        //    { "data": "valor" }
        //]
    });

    var table = $('#lista_produtos').DataTable();

    $('#lista_produtos tbody').on('click', 'tr', function () {
        document.getElementById("id").focus();
        document.getElementById('id').value = table.row(this).data();
        var valorClicado = document.getElementById('id').value;
        var valor = parseInt(valorClicado.split(/\D+/), 10);
        var data = table.row(this).data();
        document.getElementById('id').value = valor;
        window.location.assign("http://localhost:2996/Produto/Buscar/" + valor);
    });
});