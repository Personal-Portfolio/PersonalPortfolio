using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MatBlazor
{
    /// <summary>
    /// The navigation drawer slides in from the left and contains the navigation destinations for your app.
    /// </summary>
    public class BaseMatDrawer : BaseMatDomComponent
    {
        private bool _opened;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public MatDrawerMode Mode { get; set; }

        [Parameter]
        public int ContentTabIndex { get; set; }

        [Parameter]
        public bool Opened
        {
            get => _opened;
            set
            {
                if (_opened == value)
                    return;

                _opened = value;

                CallAfterRender(async () =>
                {
                    await JsInvokeAsync<object>("matBlazor.matDrawer.setOpened", Ref, _opened);
                });

                OpenedChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<bool> OpenedChanged { get; set; }

        private DotNetObjectReference<BaseMatDrawer> _dotNetObjectRef;
        public BaseMatDrawer()
        {
            ClassMapper
                .Add("mdc-drawer")
                .Add("mat-drawer")
                .If("mdc-drawer--dismissible", () => Mode == MatDrawerMode.Dismissible)
                .If("mdc-drawer--modal", () => Mode == MatDrawerMode.Modal);

            CallAfterRender(async () =>
            {
                _dotNetObjectRef ??= CreateDotNetObjectRef(this);
                await JsInvokeAsync<object>("matBlazor.matDrawer.init", Ref, _dotNetObjectRef);
            });
        }

        public override void Dispose()
        {
            base.Dispose();
            DisposeDotNetObjectRef(_dotNetObjectRef);
        }

        [JSInvokable]
        public void ClosedHandler()
        {
            StateHasChanged();
            _opened = false;
            OpenedChanged.InvokeAsync(false);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}