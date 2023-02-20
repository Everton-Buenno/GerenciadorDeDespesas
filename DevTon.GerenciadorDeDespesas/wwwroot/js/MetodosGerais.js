function PegarValores(dados) {
    var valores = [];
    var tamanho = dados.length;
    var indice = 0;

    while (indice < tamanho) {
        valores.push(dados[indice].valores);
        indice++;
    }

    return valores;
}


function PegarTiposDespesas(dados) {

    var labels = [];
    var tamanho = dados.length;
    var indice = 0;

    while (indice < tamanho) {
        labels.push(dados[indice].tiposDespesas);
        indice++;
    }

    return labels;
}

function PegarMeses(dados) {
    var labels = [];
    var tamanho = dados.length;
    var indice = 0;

    while (indice < tamanho) {
        labels.push(dados[indice].nomeMeses[0]);
        indice++;
    }

    return labels;
}



function PegarCores(quantidade) {
    var cores = [];

    while (quantidade > 0) {
        var r = Math.floor(Math.random() * 255);
        var g = Math.floor(Math.random() * 255);
        var b = Math.floor(Math.random() * 255);

        cores.push("rgb(" + r + ", " + g + "," + b + ")");

        quantidade--;
    }
    return cores;
}
