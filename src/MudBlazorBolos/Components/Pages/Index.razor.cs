using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazorBolos.Domain.Models;

namespace MudBlazorBolos.Components.Pages
{
    public partial class Index
    {

        List<Bolo> bolos = new List<Bolo>();

        protected override async Task OnInitializedAsync()
        {
            bolos = await _context.Bolos.ToListAsync();
        }

        private async Task CreateAsync()
        {
            var parameters = new DialogParameters();
            parameters.Add("Bolo", new Bolo());

            var dialog = _dialogService.Show<GerenciaBolosDialog>("Incluir", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                Bolo? novoBolo = result.Data as Bolo;
                _context.Bolos.Add(novoBolo!);
                await _context.SaveChangesAsync();
                bolos.Insert(0, novoBolo!);
                StateHasChanged();
            }
        }

        private async Task UpdateAsync(int id)
        {
            var parameters = new DialogParameters();
            var boloAtualizar = bolos.FirstOrDefault(_ => _.Id == id);
            parameters.Add("Bolo", boloAtualizar);

            var dialog = _dialogService.Show<GerenciaBolosDialog>("Atualizar", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var boloAtualizado = result.Data as Bolo;
                _context.Bolos.Update(boloAtualizado!);
                await _context.SaveChangesAsync();

                bolos.Remove(boloAtualizar!);
                bolos.Insert(0, boloAtualizado!);
                StateHasChanged();
            }
        }

        private async Task DeleteAsync(int id)
        {
            bool? result = await _dialogService.ShowMessageBox(
                "Confirma exclusão",
                "A exclusão não pode ser desfeita!",
                yesText: "Deleta", cancelText: "Cancela");

            if (result ?? false)
            {
                var boloRemover = await _context.Bolos.FindAsync(id);
                _context.Bolos.Remove(boloRemover!);
                await _context.SaveChangesAsync();
                bolos.Remove(boloRemover!);
                StateHasChanged();
            }
        }
    }
}