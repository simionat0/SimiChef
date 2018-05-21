$(document).ready(function () {
    $('#lista_usuarios').DataTable({
    });

    var table = $('#lista_usuarios').DataTable();

    $('#lista_usuarios tbody').on('click', 'tr', function () {
        document.getElementById("id_pesquisa").focus();
        document.getElementById('id_pesquisa').value = table.row(this).data();
        var valorClicado = document.getElementById('id_pesquisa').value;
        var valor = parseInt(valorClicado.split(/\D+/), 10);
        var data = table.row(this).data();
        document.getElementById('id_pesquisa').value = valor;
        window.location.assign("http://localhost:2996/Usuario/Buscar/" + valor);
    });
});