import {MDCDrawer} from '@material/drawer/index';


export function init(ref, component) {
  ref.matBlazorRef = MDCDrawer.attachTo(ref);
  ref.addEventListener('MDCDrawer:closed', () => {
    component.invokeMethodAsync('ClosedHandler');
  });
}


export function setOpened(ref, opened) {
  ref.matBlazorRef.open = opened;
}
