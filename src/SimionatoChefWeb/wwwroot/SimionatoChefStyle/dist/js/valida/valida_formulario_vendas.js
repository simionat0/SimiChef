function valida_form_venda(valid) {

    retorno = true;
    if (document.getElementById("nome").value === "") {
        alert('Você não selecionou o produto ainda.');
        document.getElementById("quantidade_produto").focus();
        retorno = false;
    }
    if (document.getElementById("quantidade").value === "") {
        alert('Por favor, preencha a quantidade desejada para o produto!');
        document.getElementById("quantidade_produto").focus();
        retorno = false;
    }
    
    var estoque = parseInt(document.getElementById('estoque').value, 10);
    var qtd = parseInt(document.getElementById('quantidade').value, 10);
    
    if (estoque < qtd) {
        alert('A quantidade informada é maior do que temos disponivel! Por favor, preencha um novo valor.');
        document.getElementById("quantidade").focus();
        retorno = false;
    }
    if (retorno) {
        document.getElementById(valid).submit();
    }

}