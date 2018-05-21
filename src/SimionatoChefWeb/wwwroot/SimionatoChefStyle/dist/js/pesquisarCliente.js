$(document).ready(function () {
    $('#lista_clientes').DataTable({
    });

    var table = $('#lista_clientes').DataTable();

    $('#lista_clientes tbody').on('click', 'tr', function () {
        document.getElementById("idPesquisaCli").focus();
        document.getElementById('idPesquisaCli').value = table.row(this).data();
        var valorClicado = document.getElementById('idPesquisaCli').value;
        var valor = parseInt(valorClicado.split(/\D+/), 10);
        var data = table.row(this).data();
        document.getElementById('idPesquisaCli').value = valor;
        window.location.assign("http://localhost:2996/Cliente/Buscar/" + valor);
    });
});