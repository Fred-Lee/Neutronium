﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

using MVVM.Component;
using MVVM.HTML.Core.Binding;
using MVVM.HTML.Core.Binding.Extension;
using MVVM.HTML.Core.Binding.Mapping;
using MVVM.HTML.Core.JavascriptEngine.JavascriptObject;

namespace MVVM.HTML.Core.HTMLBinding
{
    public class JSSimpleCommand : GlueBase, IJSObservableBridge
    {
        private readonly ISimpleCommand _JSSimpleCommand;
        private readonly IWebView _IWebView;
        private readonly IJavascriptToCSharpConverter _JavascriptToCSharpConverter;
        public JSSimpleCommand(IWebView iCefV8Context, IJavascriptToCSharpConverter converter, ISimpleCommand icValue)
        {
            _IWebView = iCefV8Context;
            _JavascriptToCSharpConverter = converter;
            _JSSimpleCommand = icValue;
            JSValue = _IWebView.Factory.CreateObject(true);
        }

        public IJavascriptObject JSValue { get; private set; }

        private IJavascriptObject _MappedJSValue;

        public IJavascriptObject MappedJSValue { get { return _MappedJSValue; } }

        public void SetMappedJSValue(IJavascriptObject ijsobject)
        {
            _MappedJSValue = ijsobject;
            _MappedJSValue.Bind("Execute", _IWebView, Execute);
        }

        private void Execute(IJavascriptObject[] e)
        {
            _JSSimpleCommand.Execute(_JavascriptToCSharpConverter.GetFirstArgumentOrNull(e));
        }

        public object CValue
        {
            get { return _JSSimpleCommand; }
        }

        public JSCSGlueType Type
        {
            get { return JSCSGlueType.SimpleCommand; }
        }

        public IEnumerable<IJSCSGlue> GetChildren()
        {
            return Enumerable.Empty<IJSCSGlue>();
        }

        protected override void ComputeString(StringBuilder sb, HashSet<IJSCSGlue> alreadyComputed)
        {
            sb.Append("{}");
        }
    }
}
