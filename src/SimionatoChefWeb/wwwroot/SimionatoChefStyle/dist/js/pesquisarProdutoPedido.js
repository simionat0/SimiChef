$(document).ready(function () {
    $('#listProdutoPedido').DataTable({
        //"ajax": 'http://localhost:2996/public_api/ListaProdutos',
        //"columns": [
        //    { "data": "id" },
        //    { "data": "nome" },
        //    { "data": "quantidade" },
        //    { "data": "valor" }
        //]
    });
    var table = $('#listProdutoPedido').DataTable();

    $('#listProdutoPedido tbody').on('click', 'tr', function () {
        document.getElementById('idProdutoVenda').focus();
        document.getElementById('idProdutoVenda').value = table.row(this).data();
        var valorClicado = document.getElementById('idProdutoVenda').value;
        var valor = parseInt(valorClicado.split(/\D+/), 10);
        document.getElementById('idProdutoVenda').value = valor;
        window.location.assign("http://localhost:2996/Venda/BuscarProduto/" + valor);
    });
});