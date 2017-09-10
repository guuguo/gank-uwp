using System;

namespace Gank.Helper
{
    public enum NavigateMode
    {
        Main,
        Master,
        Detail
    }
    public delegate void MessageHandel(object par, params object[] par1);
    public delegate void NavigateHandel(Type page, params object[] par);
    public delegate void LoginedHandel();
    public static class NavigationHelper
    {
        public static event MessageHandel ChanageThemeEvent;
        public static event LoginedHandel Logined;
        public static void SendLogined()
        {
            Logined();
        }
        public static void SendChanageThemeEvent(object par, params object[] par1)
        {
            ChanageThemeEvent(par, par1);
        }
        public static event NavigateHandel DetailNavigateToEvent;
        public static event NavigateHandel MasterNavigateToEvent;
        public static event NavigateHandel MianNavigateToEvent;
        public static event NavigateHandel HomeNavigateToEvent;
        public static void SendNavigateTo(NavigateMode mode, Type page, params object[] par)
        {
            switch (mode)
            {
                case NavigateMode.Main:
                    MianNavigateToEvent(page, par);
                    break;
                case NavigateMode.Detail:
                    DetailNavigateToEvent(page, par);
                    break;
                case NavigateMode.Master:
                    MasterNavigateToEvent(page, par);
                    break;
                default:
                    break;
            }

        }

    }
}
