package nz.co.fendustries.xtrade.fragments;

import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;

import javax.inject.Inject;

import dagger.Lazy;
import nz.co.fendustries.xtrade.R;
import nz.co.fendustries.xtrade.XtradeApplication;
import nz.co.fendustries.xtrade.activities.HomeActivity;
import nz.co.fendustries.xtrade.adapters.ConvertedRatesRecyclerAdapter;
import nz.co.fendustries.xtrade.helpers.AndroidConstants;
import nz.co.fendustries.xtrade.interfaces.CalculateViewContract;
import nz.co.fendustries.xtrade.presenters.CalculatePresenter;

/**
 * Created by joshuafenemore on 21/10/16.
 */
public class CalculateFragment extends Fragment implements CalculateViewContract
{
    private EditText valueEditText;
    private RecyclerView ratesRecyclerView;
    private RecyclerView.LayoutManager ratesRecylerViewLayoutManager;
    private ConvertedRatesRecyclerAdapter ratesRecyclerAdapter;
    private String savedString = "";

    @Inject
    Lazy<CalculatePresenter> calculatePresenterLazy;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    {
        super.onCreateView(inflater, container, savedInstanceState);
        ((XtradeApplication)this.getActivity().getApplication()).getXtradeComponent().inject(this);

        View view = inflater.inflate(R.layout.fragment_calculate, container, false);
        this.valueEditText = (EditText) view.findViewById(R.id.valueEditText);
        this.ratesRecyclerView = (RecyclerView) view.findViewById(R.id.calculatedRatesRecyclerView);
        this.ratesRecylerViewLayoutManager = new LinearLayoutManager(this.getActivity());
        this.ratesRecyclerView.setLayoutManager(this.ratesRecylerViewLayoutManager);

        if (savedInstanceState != null)
        {
            this.savedString = savedInstanceState.getString(AndroidConstants.RATE_ENTERED, "");
        }

        return view;
    }

    @Override
    public void onResume()
    {
        super.onResume();

        ((HomeActivity) this.getActivity()).setActionBarTitle(this.getString(R.string.calculateLabel));

        this.valueEditText.addTextChangedListener(this.textWatcher);

        this.calculatePresenterLazy.get().setCalculateViewContract(this, this.savedString);
    }

    @Override
    public void onPause()
    {
        super.onPause();

        InputMethodManager imm = (InputMethodManager) this.getActivity().getSystemService(Context.INPUT_METHOD_SERVICE);
        imm.hideSoftInputFromWindow(this.getActivity().getWindow().getDecorView().getWindowToken(), 0);

        this.ratesRecyclerAdapter = null;
        this.valueEditText.removeTextChangedListener(this.textWatcher);
        this.calculatePresenterLazy.get().setCalculateViewContract(null, null);
    }

    @Override
    public void onSaveInstanceState(Bundle outgoingState)
    {
        outgoingState.putString(AndroidConstants.RATE_ENTERED, this.valueEditText.getText().toString());
        super.onSaveInstanceState(outgoingState);
    }

    @Override
    public void updateView()
    {
        if (this.ratesRecyclerAdapter == null)
        {
            this.ratesRecyclerAdapter = new ConvertedRatesRecyclerAdapter((AppCompatActivity) this.getActivity(), this.calculatePresenterLazy.get().getConvertedRateViewModels());
            this.ratesRecyclerView.setAdapter(this.ratesRecyclerAdapter);
        }
        else
        {
            this.ratesRecyclerAdapter.notifyDataSetChanged();
        }

        this.valueEditText.removeTextChangedListener(this.textWatcher);
        this.valueEditText.setText(this.calculatePresenterLazy.get().getDollarValue());
        this.valueEditText.setSelection(this.calculatePresenterLazy.get().getDollarValue().length());
        this.valueEditText.addTextChangedListener(this.textWatcher);
    }

    private TextWatcher textWatcher = new TextWatcher()
    {
        @Override
        public void beforeTextChanged(CharSequence s, int start, int count, int after)
        {

        }

        @Override
        public void onTextChanged(CharSequence s, int start, int before, int count)
        {
            calculatePresenterLazy.get().updateData(s.toString());
        }

        @Override
        public void afterTextChanged(Editable s)
        {

        }
    };
}