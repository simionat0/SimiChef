$(document).ready(function () {
    $('#listProduto').DataTable({
        "ajax": '/simiInfo/manager/home/estoque/produtos/listaTodosItens.jsp'
    });
    var table = $('#listProduto').DataTable();

    $('#listProduto tbody').on('click', 'tr', function () {
        document.getElementById("produto_pesquisa_id").focus();
        document.getElementById('produto_pesquisa_id').value = table.row(this).data();
        var valorClicado = document.getElementById('produto_pesquisa_id').value;
        var valor = parseInt(valorClicado.split(/\D+/), 10);
        document.getElementById('produto_pesquisa_id').value = valor;
        document.getElementById("produto_pesquisa").submit();
    });
});